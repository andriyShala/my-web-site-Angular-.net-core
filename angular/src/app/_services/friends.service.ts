import { Injectable } from '@angular/core';
import { Observable }     from 'rxjs/Observable';
import { Http, Headers, Request, Response, RequestOptions } from '@angular/http';

import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';

import { AppConfig } from '../app.config';
@Injectable()
export class FriendsService{
    
    constructor(private http:Http,private config:AppConfig){}
    getAllFriendsByCurrentUser():Observable<any>
    {
        let currentUser = JSON.parse(localStorage.getItem('currentUser'));
		let headers = new Headers({ 'Authorization': 'Bearer ' + currentUser.token });
		
			headers.set("Username",currentUser.username);
			let options = new RequestOptions({ headers: headers });
        return this.http.get(`${this.config.apiUrl}/api/Friends`,options)
		.map((res:Response)=>res.json());
    }
    createProposition(username):Observable<any>
    {
        let currentUser = JSON.parse(localStorage.getItem('currentUser'));
		let headers = new Headers({ 'Authorization': 'Bearer ' + currentUser.token });
		headers.set("Propuser",username);
			headers.set("Username",currentUser.username);
            let options = new RequestOptions({ headers: headers });
            console.log(options);
        return this.http.post(`${this.config.apiUrl}/api/Friends`,null,options)
    }
    setPropositionAnswer(idProposition,answer)
    {
        let currentUser = JSON.parse(localStorage.getItem('currentUser'));
		let headers = new Headers({ 'Authorization': 'Bearer ' + currentUser.token });
        headers.set("idProposition",idProposition);
        headers.set("answer",answer);
			headers.set("Username",currentUser.username);
            let options = new RequestOptions({ headers: headers });
            console.log(options);
      return this.http.put(`${this.config.apiUrl}/api/Friends`,null,options);
    }
    getAllProposition():Observable<any>
    {
        let currentUser = JSON.parse(localStorage.getItem('currentUser'));
		let headers = new Headers({ 'Authorization': 'Bearer ' + currentUser.token });
		headers.set("isProposition","true");
			headers.set("Username",currentUser.username);
			let options = new RequestOptions({ headers: headers });
        return this.http.get(`${this.config.apiUrl}/api/Friends`,options)
		.map((res:Response)=>res.json());
    }
}