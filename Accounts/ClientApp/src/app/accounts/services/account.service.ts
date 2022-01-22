import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map   } from 'rxjs/operators';
import { AccountCreateModel } from '../models/account-create.model';
import { Account, toAccount } from '../models/account.model';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  constructor(
    private readonly httpClient: HttpClient,
    @Inject('BASE_URL') baseUrl: string) {
      this.apiUrl = baseUrl + 'accounts';
    }

  private readonly apiUrl: string;

  getAccounts(): Observable<Account[]> {
    return this.httpClient.get<Account[]>(this.apiUrl)
      .pipe(
        map(res => res.map(toAccount))
      );
  }

  createAccount(model: AccountCreateModel): Observable<Account> {
    return this.httpClient.post<Account>(this.apiUrl, model)
      .pipe(
        map(toAccount)
      );
  }
}
