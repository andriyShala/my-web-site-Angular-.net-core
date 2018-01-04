import { Http, Headers, RequestOptions, Response } from '@angular/http';
 
import { AppConfig } from '../app.config';
import { User } from '../_models/index';
import {UserService} from './user.service';
import { Injectable,OnInit, EventEmitter } from '@angular/core';
import { Observable, Subject } from 'rxjs/Rx';
import { WebsocketService } from './websocket.service';
import { Subscription } from 'rxjs/Subscription';
import {IRom} from '../_models/index';
import { setTimeout } from 'timers';

export interface Message {
	type:string,
	author: string,
	recipient:string,
	message: string
}
export interface MessageRom{
	username:string,
	RomId:string,
	LastMessage:string,
	LastTime:string,
	FirstName:string,
	LastName:string,
	Image:string
}


@Injectable()
export class UserMessageService implements OnInit{
	private socketSubscription: Subscription
	private	romchange: EventEmitter<MessageRom> = new EventEmitter();
	private	messageChange: EventEmitter<Message> = new EventEmitter();
	
	private messages: Subject<Message>;
	data:any={};
	 dialoges:any[];
	
	// constructor(private socket: ServerSocket,private userSer:UserService) {}
	constructor(private config:AppConfig,
		private userSer:UserService,
		private wsService: WebsocketService,
		public authHttp: Http)
		{
       	 this.messages = <Subject<Message>>wsService
			.connect('ws://'+this.config.apiUrl2)
			.map((response: MessageEvent): Message => {
				let data = response.data;
				return {
					author: data.author,
					message: data.message,
					recipient:data.recipient,
					type:data.type
				}
			});
			this.messages.subscribe(msg => {	
					if(msg.type==="message")
					{
						this.messageChange.emit(msg);
					}
					else if(msg.type==="RomCreate"){
						console.log(msg);

						var user=JSON.parse(msg.message);
						this.romchange.emit(
							{"FirstName":user.users[0].firstName,
						"Image":user.users[0].currentImage,
						"username":user.users[0].username,
						"LastName":user.users[0].lastName,
						"RomId":user.id,
						"LastMessage":user.lastMessageText,
						"LastTime":user.timeLastMessage});
					}


			});
		setTimeout(()=>this.sendMessage("","firstCon"),500);	
	}
	sendMessage(whom:string,message:string)
	{	
		this.messages.next({"author":this.userSer.getCurrentUser().username,"recipient":whom,"message":message,"type":"Message"});
	}
	getRomChangeEmitter() {
		return this.romchange;
	}
	getMessageChangeEmitter() {
		return this.messageChange;
	}
	getAllDialoges():Observable<any>
	{
		let mmess=[];
		let currentUser = JSON.parse(localStorage.getItem('currentUser'));
		let headers = new Headers({ 'Authorization': 'Bearer ' + currentUser.token });
		headers.set("Username",currentUser.username);
		let options = new RequestOptions({ headers: headers });
		
		
		return this.authHttp.get(`${this.config.apiUrl}/api/Roms`,options)
		.map((res:Response)=>{
			return <any>res.json();
		}).catch(this.handleError);
	}
	private handleError(error: Response) {
		return Observable.throw(error.statusText);
	}
	getData(options:RequestOptions){
		return this.authHttp.get(`${this.config.apiUrl}/api/Messages`,options)
		.map((res:Response)=>res.json());
	}
	getMessagesByRomId(id:number):Observable<any>
	{
		let currentUser = JSON.parse(localStorage.getItem('currentUser'));
		let headers = new Headers({ 'Authorization': 'Bearer ' + currentUser.token });
		headers.set('RomId',""+id);
			headers.set("Username",currentUser.username);
			let options = new RequestOptions({ headers: headers });
			return this.getData(options);
	}
	getRecipientByRomId(id:number):Observable<any>
	{
		let currentUser = JSON.parse(localStorage.getItem('currentUser'));
		let headers = new Headers({ 'Authorization': 'Bearer ' + currentUser.token });
		headers.set('id',""+id);
		headers.set("Username",currentUser.username);
		let options = new RequestOptions({ headers: headers });
		return this.authHttp.get(`${this.config.apiUrl}/api/Roms`,options).map((res:Response)=>res.json());
	}
	ngOnInit() {
	
	  }
	
	  ngOnDestroy() {
		 
		  
		 this.messages.unsubscribe();
	  }
}