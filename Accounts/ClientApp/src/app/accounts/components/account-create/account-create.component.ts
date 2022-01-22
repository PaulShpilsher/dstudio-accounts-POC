import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountCreateModel } from '../../models';
import { AccountService } from '../../services/account.service';

@Component({
  selector: 'app-account-create',
  templateUrl: './account-create.component.html',
  styleUrls: ['./account-create.component.css']
})
export class AccountCreateComponent implements OnInit {
  constructor(
    private router: Router,
    private accountService: AccountService) {
  }

  // TODO: Do something with API errors.
  errorMessage!: string;

  ngOnInit(): void {
  }

  createAccount(model: AccountCreateModel): void {
    this.errorMessage = ''
    this.accountService.createAccount(model).subscribe({
      next: data => {
        console.log("Account created", data);
        setTimeout(() => this.leave(), 0);
      },
      error: err => {
        console.error(err);
        this.errorMessage = `Не удалось создать счет: ${err}`;
      }
    });
  }

  leave(): void {
    this.router.navigateByUrl("/accounts");
  }
}
