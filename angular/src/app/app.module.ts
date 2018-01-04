import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
 
import { AppComponent } from './app.component';
import { routing } from './app.routing';
import { AppConfig } from './app.config';
 
import { AlertComponent,ModalComponent } from './_directives/index';
import { AuthGuard } from './_guards/index';
import { AlertService,FriendsService, AuthenticationService, UserService,ModalService,UserMessageService,WebsocketService } from './_services/index';
import { HomeComponent } from './home/index';
import { LoginComponent } from './login/index';
import {NavComponent,NavLoginComponent} from './components/index';
import { RegisterComponent } from './register/index';
import {MyPageComponent,SearchPersonComponent,MyMessageModule,NavMenuComponent,StatusBarComponent,MyFriendModule} from "./components/pages/index";
import { LazyLoadImagesModule } from 'ngx-lazy-load-images';
@NgModule({
    imports: [
        LazyLoadImagesModule,
        MyFriendModule,
        BrowserModule,
        FormsModule,
        HttpModule,
        MyMessageModule,
        routing
    ],
    declarations: [
        SearchPersonComponent,
        StatusBarComponent,
        ModalComponent,
        AppComponent,
        NavComponent,
        MyPageComponent,
        NavLoginComponent,
        NavMenuComponent,
        AlertComponent,
        HomeComponent,
        LoginComponent,
        RegisterComponent
    ],
    providers: [
        FriendsService,
        ModalService,
        UserMessageService,
        WebsocketService,
        AppConfig,
        AuthGuard,
        AlertService,
        AuthenticationService,
        UserService
    ],
    
    bootstrap: [AppComponent]
})
 
export class AppModule { }
