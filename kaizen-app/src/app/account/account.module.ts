import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SharedModule } from '../shared/shared.module';

import { UserService } from '../../_services/user.service';

import { EmailValidatorDirective } from '../directives/email.validator.directive';

import { routing } from './account.routing';
import { RegistrationFormComponent } from './registration-form/registration-form.component';
import { LoginFormComponent } from './login-form/login-form.component';
// import { FacebookLoginComponent } from './facebook-login/facebook-login.component';


@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    routing,
    SharedModule
  ],
  declarations: [
    RegistrationFormComponent,
    EmailValidatorDirective,
    LoginFormComponent,
    // FacebookLoginComponent
  ],
  providers:    [ UserService ]
})
export class AccountModule {
}
