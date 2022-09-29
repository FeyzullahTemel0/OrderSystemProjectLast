import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { Order } from './models/Order';
import { OrderService } from './services/Order.service';
import { environment } from 'environments/environment';
import { OrderDetailsDto } from './models/orderdetailsdto';
import { Customer } from '../customer/models/Customer';
import { CustomerService } from '../customer/services/Customer.service';
import { ProductService } from '../product/services/product.service'
import { Product } from '../product/models/Product';
import { QualityControlTypeEnumLabelMapping } from '../product/models/Enums';
import { LookUp } from 'app/core/models/lookUp';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { MatAutocompleteModule } from '@angular/material/autocomplete';


declare var jQuery: any;

@Component({
	selector: 'app-order',
	templateUrl: './order.component.html',
	styleUrls: ['./order.component.scss'],
	providers:[MatAutocompleteModule]
})
export class OrderComponent implements AfterViewInit, OnInit {

	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['id', 'customerName', 'productName', 'size', 'amount', 'status', 'update', 'delete'];//***

	orderList: OrderDetailsDto[];
	order: Order = new Order();
	orderAddForm: FormGroup;
	customer: Customer[];
	product: Product[];
	//autoComplate LookUp
	customerlookUp: Customer[] = [];
	productlookUp: Product[] = [];
	orderId: number;
	sizelookUp: LookUp[] = [];
	sizess: string[] = Object.keys(QualityControlTypeEnumLabelMapping);
	//AuthoComplates
	filterProduct: Observable<Product[]>;
	filterCustomer: Observable<Customer[]>;

	constructor(private orderService: OrderService, private customerService: CustomerService, private productService: ProductService, private lookupService: LookUpService, private alertifyService: AlertifyService, private formBuilder: FormBuilder, private authService: AuthService) { }

	ngAfterViewInit(): void {
		this.getOrderDetailsList();//***
	}

	ngOnInit() {
		this.createOrderAddForm();
		this.authService.getCurrentUserId();
		this.getCustomerList();
		this.getProductList();
		this.sizess.forEach(element => {
			this.sizelookUp.push({ id: [Number(element)], label: QualityControlTypeEnumLabelMapping[Number(element)] });
			console.log(this.sizelookUp);

		});

	}

	//Customer AutoComplate
	private _filter1(value: string): Customer[] {
		const filterValue1 = value.toLowerCase();

		return this.customerlookUp.filter(option => option.customerName.toLowerCase().includes(filterValue1));
	}

	displayFn1(customer: Customer): string {
		return customer && customer.customerName ? customer.customerName : '';
	}

	getCustomerList() {
		this.customerService.getCustomerList().subscribe(data => {
			this.customerlookUp = data;
			this.filterCustomer = this.orderAddForm.controls.customerId.valueChanges.pipe(
				startWith(''),
				map(value1 => typeof value1 === 'string' ? value1 : value1.customerName),
				map(name1 => name1 ? this._filter1(name1) : this.customerlookUp.slice())
			);
		})
	}

	//***

	private _filter(value: string): Product[] {
		const filterValue = value.toLowerCase();

		return this.productlookUp.filter(option => option.name.toLowerCase().includes(filterValue));
	}
	displayFn(product: Product): string {
		return product && product.name ? product.name : '';
	}
	
	getProductList() {
		this.productService.getProductList().subscribe(data => {
			this.productlookUp = data;

			this.filterProduct = this.orderAddForm.controls.productId.valueChanges.pipe(
				startWith(''),
				map(value => typeof value === 'string' ? value : value.productName),
				map(name => name ? this._filter(name) : this.productlookUp.slice())
			);
		})
	}

	getOrderDetailsList() {
		this.orderService.getOrderDetailsList().subscribe(data => {
			this.orderList = data;
			console.log(this.orderList);
			this.dataSource = new MatTableDataSource(data);
			this.configDataTable();
		})
	}

	save() {

		if (this.orderAddForm.valid) {
			this.order = Object.assign({}, this.orderAddForm.value)
			this.order.productId = this.order.productId['id'];
			this.order.customerId = this.order.customerId['id'];
			console.log(this.order)
			if (this.order.id == 0) {
				this.order.createdUserId = this.authService.getCurrentUserId();
				this.order.lastUpdatedUserId = this.authService.getCurrentUserId();
				console.log(this.order);

				this.addOrder();
			}
			else {
				this.order.lastUpdatedUserId = this.authService.getCurrentUserId();
				this.updateOrder();
			}
		}

	}

	addOrder() {

		this.orderService.addOrder(this.order).subscribe(data => {
			this.getOrderDetailsList();
			this.order = new Order();
			jQuery('#order').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.orderAddForm);
			this.getOrderDetailsList();
		}, error => {
			this.alertifyService.error("You Entered Wrong Data Try Again");
		})

	}

	updateOrder() {

		this.orderService.updateOrder(this.order).subscribe(data => {

			var index = this.orderList.findIndex(x => x.id == this.order.id);
			this.orderList[index] = this.order;
			this.dataSource = new MatTableDataSource(this.orderList);
			this.configDataTable();
			this.order = new Order();
			jQuery('#order').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.orderAddForm);
			this.getOrderDetailsList();
			console.log(this.orderList);

		})

	}

	createOrderAddForm() {
		this.orderAddForm = this.formBuilder.group({
			id: [0],
			createdUserId: [],
			lastUpdatedUserId: [],
			status: [true],
			customerId: [],
			productId: [],
			size: [],
			amount: [0]
		})
	}

	deleteOrder(orderId: number) {
		this.orderService.deleteOrder(orderId).subscribe(data => {
			this.alertifyService.success(data.toString());
			this.orderList = this.orderList.filter(x => x.id != orderId);
			this.dataSource = new MatTableDataSource(this.orderList);
			this.configDataTable();
		})
	}

	getOrderById(orderId: number) {
		this.clearFormGroup(this.orderAddForm);
		this.orderService.getOrderById(orderId).subscribe(data => {
			this.order = data;
			this.orderAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'id')
				group.get(key).setValue(0);
			if (key == 'customerName')
				group.get(key).setValue(0);
			if (key == 'productName')
				group.get(key).setValue(0);
			if (key == 'size')
				group.get(key).setValue(0);
			if (key == 'amount')
				group.get(key).setValue(0);
			if (key == 'status')
				group.get(key).setValue(true);

		});
	}

	checkClaim(claim: string): boolean {
		return this.authService.claimGuard(claim)
	}

	configDataTable(): void {
		this.dataSource.paginator = this.paginator;
		this.dataSource.sort = this.sort;
	}

	applyFilter(event: Event) {
		const filterValue = (event.target as HTMLInputElement).value;
		this.dataSource.filter = filterValue.trim().toLowerCase();

		if (this.dataSource.paginator) {
			this.dataSource.paginator.firstPage();
		}
	}

	getSizeLabel(id: number) {
		return QualityControlTypeEnumLabelMapping[id]
	}

}
