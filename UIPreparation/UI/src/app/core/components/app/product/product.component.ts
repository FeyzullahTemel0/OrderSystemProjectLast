import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { Product } from './models/Product';
import { ProductService } from './services/Product.service';
import { QualityControlTypeEnumLabelMapping, Size } from './models/Enums';
import { environment } from 'environments/environment';
import { LookUp } from 'app/core/models/lookUp';
import { element } from 'protractor';

declare var jQuery: any;

@Component({
	selector: 'app-product',
	templateUrl: './product.component.html',
	styleUrls: ['./product.component.scss']
})
export class ProductComponent implements AfterViewInit, OnInit {

	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['id',  'name', 'color', 'size','status', 'update', 'delete'];

	productList: Product[];
	productName: Product[];//***
	product: Product = new Product() ;
	productAddForm: FormGroup;
	Size :string[] = Object.keys(QualityControlTypeEnumLabelMapping);
	sizeLookUp: LookUp[]=[];

	productId: number;

	constructor(private productService: ProductService, private lookupService: LookUpService, private alertifyService: AlertifyService, private formBuilder: FormBuilder, private authService: AuthService) { }

	ngAfterViewInit(): void {
		this.getProductList();
	}

	ngOnInit() {

		this.createProductAddForm();
		this.Size.forEach(data => {
			//***
			this.sizeLookUp.push({id:[Number(data)],label:QualityControlTypeEnumLabelMapping[Number(data)]});
			
			// console.log(QualityControlTypeEnumLabelMapping[Number(data)]);

		});
	}

	getSizeName(id:number){

		return QualityControlTypeEnumLabelMapping[id];
	}


	getProductList() {
		this.productService.getProductList().subscribe(data => {
			this.productList = data;
			this.dataSource = new MatTableDataSource(data);
			this.configDataTable();
		});
	}

	save() {

		if (this.productAddForm.valid) {
			this.product = Object.assign({}, this.productAddForm.value)

			if (this.product.id == 0)
				this.addProduct();

			else
				this.updateProduct();
		}

	}

	addProduct() {

		this.productService.addProduct(this.product).subscribe(data => {
			this.getProductList();
			this.product = new Product();
			jQuery('#product').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.productAddForm);

		})

	}

	updateProduct() {

		this.productService.updateProduct(this.product).subscribe(data => {

			var index = this.productList.findIndex(x => x.id == this.product.id);
			this.productList[index] = this.product;
			this.dataSource = new MatTableDataSource(this.productList);
			this.configDataTable();
			this.product = new Product();
			jQuery('#product').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.productAddForm);

		})

	}

	createProductAddForm() {
		this.productAddForm = this.formBuilder.group({
			id: [0],
			createdUserId: [this.authService.getCurrentUserId],
			lastUpdatedUserId: [this.authService.getCurrentUserId],
			status: [true],
			name: [""],
			color: [""],
			size: [""]
		})
	}

	deleteProduct(productId: number) {
		this.productService.deleteProduct(productId).subscribe(data => {
			this.alertifyService.success(data.toString());
			this.productList = this.productList.filter(x => x.id != productId);
			this.dataSource = new MatTableDataSource(this.productList);
			this.configDataTable();
		})
	}

	getProductById(productId: number) {
		this.clearFormGroup(this.productAddForm);
		this.productService.getProductById(productId).subscribe(data => {
			this.product = data;
			this.productAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);

			if (key == 'id')
				group.get(key).setValue("");

			if(key == 'status')
			group.get(key).setValue("");
			
			if(key == 'name')
			group.get(key).setValue("");

			if(key == 'color')
			group.get(key).setValue("");

			if(key == 'size')
			group.get(key).setValue("");

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
