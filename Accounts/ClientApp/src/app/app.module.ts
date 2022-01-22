import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { AccountCreateFormComponent } from './accounts/components/account-create-form/account-create-form.component';
import { AccountCreateComponent } from './accounts/components/account-create/account-create.component';
import { AccountListComponent } from './accounts/components/account-list/account-list.component';
import { AccountOverviewComponent } from './accounts/components/account-overview/account-overview.component';

@NgModule({
  declarations: [
    AppComponent,
    AccountCreateFormComponent,
    AccountListComponent,
    AccountCreateComponent,
    AccountOverviewComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    MatTableModule,
    MatSortModule,
    MatProgressSpinnerModule,
    MatInputModule,
    MatInputModule,
    MatFormFieldModule,
    MatButtonModule,
    MatSelectModule,
    RouterModule.forRoot([
      { path: '', redirectTo: 'accounts',  pathMatch: 'full' },
      { path: 'accounts', component: AccountOverviewComponent },
      { path: 'new-account', component: AccountCreateComponent }
    ]),
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
