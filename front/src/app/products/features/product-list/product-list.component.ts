import { Component, OnInit, ViewChild, inject } from "@angular/core";
import { CartService } from "app/cart/cart.service";
import { Product, InventoryStatus } from "app/products/data-access/product.model";
import { ProductsService } from "app/products/data-access/products.service";
import { ButtonModule } from "primeng/button";
import { CardModule } from "primeng/card";
import { DataViewModule } from 'primeng/dataview';
import { TagModule } from 'primeng/tag';
import { InputTextModule } from "primeng/inputtext";
import { DataView } from "primeng/dataview";
import { InputNumberModule } from "primeng/inputnumber";
import { FormsModule } from "@angular/forms";
import { CommonModule } from "@angular/common";

@Component({
  selector: "app-product-list",
  templateUrl: "./product-list.component.html",
  styleUrls: ["./product-list.component.scss"],
  standalone: true,
  imports: [DataViewModule, CardModule, ButtonModule, TagModule, InputTextModule, InputNumberModule, FormsModule, CommonModule],
})
export class ProductListComponent implements OnInit {
  @ViewChild('dv') dv: DataView | undefined;
  private readonly productsService = inject(ProductsService);
  private readonly cartService = inject(CartService);
  public readonly InventoryStatus = InventoryStatus;
  public readonly products = this.productsService.products;

  ngOnInit() {
    this.productsService.get().subscribe();
  }

  addToCart(product: Product) {
    this.cartService.addToCart(product);
  }

  applyFilterGlobal($event: Event, filterMatchMode: string = "contains") {
    const inputValue = ($event.target as HTMLInputElement).value;
    this.dv!.filter(inputValue, filterMatchMode);
  }

  getProductQuantityInCart(productId: number) {
    return this.cartService.getProductQuantityInCart(productId);
  }

  onQuantityChange(product: Product, newQuantity: number) {
    this.cartService.updateCart(product, newQuantity);
  }
}
