import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Account, getAccountStatusLookup, getPaymentMethodLookup } from '../../models';
import { AccountService } from '../../services/account.service';

@Component({
  selector: 'app-account-list',
  templateUrl: './account-list.component.html',
  styleUrls: ['./account-list.component.css']
})
export class AccountListComponent implements OnInit, AfterViewInit {
  constructor(private readonly accountService: AccountService) {
  }

  //accounts: Account[] = [];

  public readonly displayedColumns = [
    'created',
    'accountNumber',
    'status',
    'amount',
    'paymentMethod'
  ];

  readonly dataSource = new MatTableDataSource<Account>();
  @ViewChild(MatSort, { static: false }) sort!: MatSort;

  paymentMethods = getPaymentMethodLookup();
  accountStatuses = getAccountStatusLookup();

  ngOnInit(): void {
    this.getAccounts();
  }

  ngAfterViewInit() {
    this.dataSource.sort = this.sort;
  }

  getAccounts(): void {
    this.accountService.getAccounts()
      .subscribe((data: Account[]) => {
        console.log(data);
        this.dataSource.data = data;
      })
  }
}

