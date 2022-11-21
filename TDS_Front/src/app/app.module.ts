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
import { NavbarUserComponent as MaestrosNavbar } from './Maestros/navbar-user/navbar-user.component';
import { LoginGuard } from './Institucion/auth/login.guard';
import { RegistroComponent as UsuarioRegistro } from './Login/registro/registro.component'
import { IndexComponent as UsuarioIndex } from './Login/index/index.component';
import { LoginComponent as UsuarioLogin } from './Login/login/login.component';
import { IndexComponent as MaestrosIndex } from './Maestros/index/index.component';
import { FormComponent as MaestrosForm } from './Maestros/form/form.component';
import { IndexUserComponent as MaestroUserIndex } from './Maestros/index-user/index-user.component';
import { EstudiantesComponent as MaestroUserEstudiantes } from './Maestros/estudiantes/estudiantes.component';
import { TareasComponent as MaestrosUserTareas } from './Maestros/tareas/tareas.component';
import { IndexComponent as EstudiantesIndex } from './Estudiantes/index/index.component';
import { FormComponent as EstudiantesForm } from './Estudiantes/form/form.component';
import { IndexUserComponent as EstudianteUserIndex } from './Estudiantes/index-user/index-user.component';
import { TareaComponent as EstudiantesUserTarea } from './Estudiantes/tarea/tarea.component';
import { IndexComponent as ClasesIndex } from './Clases/index/index.component';
import { FormComponent as ClasesForm } from './Clases/form/form.component';
import { LoginGuard as UserLogin } from './Auth/login.guard';
import { NavbarUserComponent as NavbarUserMaestros } from './Maestros/navbar-user/navbar-user.component';
import { NavbarUserComponent as NavbarUserEstudiantes } from './Estudiantes/navbar-user/navbar-user.component';

const routes: Routes = [
  { path: "", redirectTo: "usuario/login", pathMatch: "full" },
  { path: "institucion/registro", component: RegistroComponent },
  { path: "institucion/login", component: LoginComponent },
  { path: "institucion", component: IndexComponent, canActivate: [LoginGuard] },
  { path: "maestros", component: MaestrosIndex, canActivate: [LoginGuard] },
  { path: "maestrosForm", component: MaestrosForm, canActivate: [LoginGuard] },
  { path: "maestrosForm/:id", component: MaestrosForm, canActivate: [LoginGuard] },
  { path: "maestroUser", component: MaestroUserIndex, canActivate: [UserLogin] },
  { path: "maestroUser/clases/estudiantes/:id", component: MaestroUserEstudiantes, canActivate: [UserLogin] },
  { path: "maestroUser/clases/tareas/:id", component: MaestrosUserTareas, canActivate: [UserLogin] },
  { path: "estudiantes", component: EstudiantesIndex, canActivate: [LoginGuard] },
  { path: "estudiantesForm", component: EstudiantesForm, canActivate: [LoginGuard] },
  { path: "estudiantesForm/:id", component: EstudiantesForm, canActivate: [LoginGuard] },
  { path: "estudianteUser", component: EstudianteUserIndex, canActivate: [UserLogin] },
  { path: "estudianteUser/tarea", component: EstudiantesUserTarea, canActivate: [UserLogin] },
  { path: "estudianteUser/tarea/:id", component: EstudiantesUserTarea, canActivate: [UserLogin] },
  { path: "clases", component: ClasesIndex, canActivate: [LoginGuard] },
  { path: "clasesForm", component: ClasesForm, canActivate: [LoginGuard] },
  { path: "clasesForm/:id", component: ClasesForm, canActivate: [LoginGuard] },
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
    UsuarioLogin,
    MaestrosIndex,
    MaestrosForm,
    EstudiantesIndex,
    EstudiantesForm,
    MaestrosForm,
    ClasesIndex,
    ClasesForm,
    MaestroUserIndex,
    NavbarUserMaestros,
    MaestroUserEstudiantes,
    MaestrosUserTareas,
    EstudianteUserIndex,
    NavbarUserEstudiantes,
    EstudiantesUserTarea
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
