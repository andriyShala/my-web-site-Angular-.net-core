import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import{FormsModule}from "@angular/forms";

import { MyMessageComponent } from "./mymessage-list/mymessage.component";
import { MymessageDetailsComponent } from "./mymessage-details/mymessage-details.component";
import { CommonModule } from "@angular/common";
import {MymessageRoutingModule} from './mymessage-routing.module';
import{ChatPersonComponent}from './chat/chat.component';

@NgModule({
    imports: [
        CommonModule,
        MymessageRoutingModule,FormsModule
    ],
    declarations: [
        MymessageDetailsComponent,
        MyMessageComponent,
        ChatPersonComponent
    ]
})
export class MyMessageModule { }
