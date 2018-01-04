import {Injectable} from '@angular/core';
import {Router,CanActivate,ActivatedRouteSnapshot,RouterStateSnapshot} from '@angular/router';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { AppConfig } from '../app.config';

@Injectable()
export class AuthGuard implements CanActivate{
    constructor(private router:Router,private http:Http,private config:AppConfig){}
    canActivate(route:ActivatedRouteSnapshot,state:RouterStateSnapshot){
        
        if(localStorage.getItem('currentUser')){
            return true;
        }
        this.router.navigate(['/login'],{queryParams:{returnUrl:state.url}});
        return false;
    }
}