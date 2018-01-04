import { Routes, RouterModule } from '@angular/router';
 
import { HomeComponent } from './home/index';
import { LoginComponent } from './login/index';
import {MyPageComponent} from "./components/pages/mypage/mypage.component";
import { RegisterComponent } from './register/index';
import {SearchPersonComponent} from './components/pages/index';
import { AuthGuard } from './_guards/index';
 
const appRoutes: Routes = [
    { path: '', component: MyPageComponent, canActivate: [AuthGuard] },
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    {path:'mypage',component:MyPageComponent,canActivate:[AuthGuard]},
    {path:'searchPerson/:id',component:SearchPersonComponent,canActivate:[AuthGuard]},
 
    // otherwise redirect to home
    { path: '**', redirectTo: '' }
];
 
export const routing = RouterModule.forRoot(appRoutes);