import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AccountCreateModel, getPaymentMethodLookup } from '../../models';

@Component({
  selector: 'app-account-create-form',
  templateUrl: './account-create-form.component.html',
  styleUrls: ['./account-create-form.component.css']
})
export class AccountCreateFormComponent implements OnInit {
  constructor(private formBuilder: FormBuilder) {
  }

  @Output() onCreated = new EventEmitter<AccountCreateModel>();
  @Output() onCancelled = new EventEmitter();
  accountForm!: FormGroup;
  paymentMethods = getPaymentMethodLookup();

  ngOnInit(): void {
    this.accountForm = this.formBuilder.group({
      accountNumber: [null, [Validators.required, Validators.pattern(/^[0-9]*$/)]],
      amount: [null, [Validators.required, Validators.pattern(/^\d+([.]\d{0,2})?$/)]],
      paymentMethod: [null, Validators.required]
    });
  }

  onSubmit() {
    if (!this.accountForm.valid) {
      return;
    }

    const payload: AccountCreateModel = {
      ...this.accountForm.value
    };
    console.log(payload);
    this.onCreated.emit(payload);
  }

  onCancel() {
    this.onCancelled.emit();
  }
}
