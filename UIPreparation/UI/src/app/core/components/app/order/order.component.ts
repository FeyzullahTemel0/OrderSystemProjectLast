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
import { OrderDetails } from './models/orderdetailsdto';
import { Customer } from '../customer/models/Customer';
import { CustomerService } from '../customer/services/Customer.service';
import { ProductService } from '../product/services/product.service'
import { Product } from '../product/models/Product';

declare var jQuery: any;

@Component({
	selector: 'app-order',
	templateUrl: './order.component.html',
	styleUrls: ['./order.component.scss']
})
export class OrderComponent implements AfterViewInit, OnInit {

	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['id', 'customerName', 'productName', 'amount', 'status','update', 'delete'];//***

	orderList: OrderDetails[];
	order: Order = new Order();
	orderAddForm: FormGroup;
	customer:Customer[];
	product:Product[];
	
	orderId: number;

	constructor(private orderService: OrderService,private customerService: CustomerService,private productService:ProductService, private lookupService: LookUpService, private alertifyService: AlertifyService, private formBuilder: FormBuilder, private authService: AuthService) { }

	ngAfterViewInit(): void {
		this.getOrderDetailsList();//***
	}

	ngOnInit() {

		this.createOrderAddForm();
		this.getCustomerList();
		this.getProductList();

	}

	
	getCustomerList() {
		this.customerService.getCustomerList().subscribe(data => {
			this.customer = data;
			console.log(data);
		});
	}
	//***
	getProductList(){
		this.productService.getProductList().subscribe(data=>{
			this.product = data;
			console.log(this.product);
		})

	}

	getOrderDetailsList() {
		this.orderService.getOrderDetailsList().subscribe(data => {
			this.orderList = data;
			//console.log(this.orderList);
			
			this.dataSource = new MatTableDataSource(data);
			this.configDataTable();

		})
	}

	save() {

		if (this.orderAddForm.valid) {
			this.order = Object.assign({}, this.orderAddForm.value)
			console.log(this.order)
			if (this.order.id == 0) {
				this.order.createdUserId = this.authService.getCurrentUserId();
				this.order.lastUpdatedUserId = this.authService.getCurrentUserId();
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
			console.log(this.orderAddForm)

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

}
