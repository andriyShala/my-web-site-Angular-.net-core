import { Injectable } from '@angular/core';
import { Observable }     from 'rxjs/Observable';
import { Http, Headers, Request, Response, RequestOptions } from '@angular/http';

import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';

import { AppConfig } from '../app.config';
import { User } from '../_models/index';
@Injectable()
export class ImageService{
    
    constructor(private http:Http,private config:AppConfig){}
    
    // getCurrentUser()
    // {
    //     var us:User=JSON.parse(localStorage.getItem("currentUser"));
    //     return us;
    // }
    
    
    // getAll() {
    //     return this.http.get(this.config.apiUrl + '/users', this.jwt()).map((response: Response) => response.json());
    // }
 
    // getById(_id: string) {
    //     return this.http.get(this.config.apiUrl + '/users/' + _id, this.jwt()).map((response: Response) => response.json());
    // }
    
    create(body = null,
        formData: FormData = new FormData()) {


            let endPoint = '/Image/profileImage'; //use your own API endpoint
            let headers = new Headers();

           
            headers.set('Content-Type', 'application/octet-stream');
            headers.set('Upload-Content-Type', body.type);
            let currentUser = JSON.parse(localStorage.getItem('currentUser'));
            if (currentUser && currentUser.token) {
                headers.set('Authorization','Bearer'+currentUser.token);
            }

         
        return this.http.post(this.config.apiUrl + '/Image', formData, new RequestOptions({ headers: headers }));
    }
 
    update(user: User) {
        return this.http.put(this.config.apiUrl + '/users/' + user._id, user, this.jwt());
    }
 
    // delete(_id: string) {
    //     return this.http.delete(this.config.apiUrl + '/users/' + _id, this.jwt());
    // }
 
    // private helper methods
 
    private jwt() {
        // create authorization header with jwt token
        let currentUser = JSON.parse(localStorage.getItem('currentUser'));
        if (currentUser && currentUser.token) {
            let headers = new Headers({ 'Authorization': 'Bearer ' + currentUser.token });
            return new RequestOptions({ headers: headers });
        }
    }
    private makeRequest (endPoint: string,
        method: string, body = null,
        headerse: Headers = new Headers()): Observable<any>
        {
        let url = this.config.apiUrl + endPoint;
       
        if (method == 'GET') {
        let options = new RequestOptions({ headers: headerse });
        return this.http.get(url, options)
                .map(this.extractData)
                .catch(this.extractError);
        } else if (method == 'POST') {
        let options = new RequestOptions({ headers: headerse });
        return this.http.post(url, body, options)
                .map(this.extractData)
                .catch(this.extractError);
        }
    }

        private extractData (res: Response) {
            let body = res.json();
            return body.response || { };
        }
        
      private extractError (res: Response) {
            let errMsg = 'Error received from the API';
            return errMsg;
        }
      
      private handleSuccess(response) {
        console.log('Successfully uploaded image');
      }
      
      private handleError(errror) {
        console.error('Error uploading image')
      }
}