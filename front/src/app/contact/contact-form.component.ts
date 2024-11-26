import { Component } from "@angular/core";
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { FormsModule } from "@angular/forms";
import { ButtonModule } from "primeng/button";
import { DataViewModule } from 'primeng/dataview';
import { InputTextModule } from "primeng/inputtext";
import { InputTextareaModule } from "primeng/inputtextarea";
import { MessageService } from 'primeng/api';
import { ToastModule } from 'primeng/toast';

@Component({
  selector: "contact-form",
  templateUrl: "./contact-form.component.html",
  styleUrls: ["./contact-form.component.scss"],
  standalone: true,
  imports: [DataViewModule, ButtonModule, FormsModule, InputTextModule, InputTextareaModule, ReactiveFormsModule, ToastModule],
  providers: [MessageService]
})
export class ContactFormComponent {
  constructor(private messageService: MessageService) { }

  contactForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    message: new FormControl('', [Validators.required, Validators.minLength(10), Validators.maxLength(300)]),
  });

  handleSubmit() {
    if (this.contactForm.valid) {
      this.messageService.add({
        severity: 'success',
        detail: 'Demande de contact envoyée avec succès'
      });
      this.contactForm.reset();
    }
  }
}
