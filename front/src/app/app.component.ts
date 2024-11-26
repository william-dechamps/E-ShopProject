import {
  Component,
  inject,
  OnInit,
} from "@angular/core";
import { RouterModule } from "@angular/router";
import { SplitterModule } from 'primeng/splitter';
import { ToolbarModule } from 'primeng/toolbar';
import { PanelMenuComponent } from "./shared/ui/panel-menu/panel-menu.component";
import { Observable } from "rxjs";
import { CartService } from "./cart/cart.service";
import { AsyncPipe } from "@angular/common";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.scss"],
  standalone: true,
  imports: [RouterModule, SplitterModule, ToolbarModule, PanelMenuComponent, AsyncPipe],
})
export class AppComponent implements OnInit {
  private readonly cartService = inject(CartService);
  title = "E-ShopProject";
  cartItems$!: Observable<number>;

  ngOnInit(): void {
    this.cartItems$ = this.cartService.cartSubject$;
  }
}
