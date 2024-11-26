import { Routes } from "@angular/router";
import { HomeComponent } from "./shared/features/home/home.component";
import { CartComponent } from "./cart/cart.component";
import { ContactFormComponent } from "./contact/contact-form.component";

export const APP_ROUTES: Routes = [
  {
    path: "home",
    component: HomeComponent,
  },
  {
    path: "products",
    loadChildren: () =>
      import("./products/products.routes").then((m) => m.PRODUCTS_ROUTES)
  },
  {
    path: "cart",
    component: CartComponent,
  },
  {
    path: "contact",
    component: ContactFormComponent,
  },
  { path: "", redirectTo: "home", pathMatch: "full" },
];
