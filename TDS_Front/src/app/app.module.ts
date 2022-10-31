import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule, Routes } from '@angular/router';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RegistroComponent } from './Institucion/registro/registro.component';
import { HttpClientModule } from '@angular/common/http';
import { LoginComponent } from './Institucion/login/login.component';
import { IndexComponent } from './Institucion/index/index.component';
import { NavbarComponent } from './Institucion/navbar/navbar.component';
import { LoginGuard } from './Institucion/auth/login.guard';
import { RegistroComponent as UsuarioRegistro } from './Login/registro/registro.component'
import { IndexComponent as UsuarioIndex } from './Login/index/index.component';
import { LoginComponent as UsuarioLogin } from './Login/login/login.component';

const routes: Routes = [
  { path: "institucion/registro", component: RegistroComponent },
  { path: "institucion/login", component: LoginComponent },
  { path: "institucion", component: IndexComponent, canActivate: [LoginGuard] },
  { path: "usuario", component: UsuarioIndex, canActivate: [LoginGuard] },
  { path: "usuario/registro", component: UsuarioRegistro, canActivate: [LoginGuard] },
  { path: "usuario/registro/:id", component: UsuarioRegistro, canActivate: [LoginGuard] },
  { path: "usuario/login", component: UsuarioLogin },
];

@NgModule({
  declarations: [
    AppComponent,
    RegistroComponent,
    LoginComponent,
    IndexComponent,
    NavbarComponent,
    UsuarioRegistro,
    UsuarioIndex,
    UsuarioLogin
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    RouterModule.forRoot(routes),
    FormsModule,
    HttpClientModule
  ],
  exports: [RouterModule],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
