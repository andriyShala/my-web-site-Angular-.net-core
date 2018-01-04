import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import{FormsModule}from "@angular/forms";

import { CommonModule } from "@angular/common";
import{MymessageRoutingModule} from './myfriend-routing.module';
import {MyFriendHomeComponent} from './myfriend-home/myfriend-home.component';
import{MyFriendListComponent} from './myfriend-list/myfriend-list.component';
import{MyFriendPropositionComponent} from './myfriend-proposition/myfriend-proposition.component';
import { ModalService } from "../../../_services/index";
import { ModalSComponent } from "../../../_directives/modalS.component";
@NgModule({
    imports: [
        CommonModule,
        MymessageRoutingModule,FormsModule
    ],
    declarations: [
        ModalSComponent,
        MyFriendPropositionComponent,
        MyFriendListComponent,
        MyFriendHomeComponent
    ]
    ,providers:[
        ModalService
    ]
})
export class MyFriendModule { }
