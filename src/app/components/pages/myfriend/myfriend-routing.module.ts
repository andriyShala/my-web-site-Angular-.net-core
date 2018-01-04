import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";

import { MyFriendListComponent } from "./myfriend-list/myfriend-list.component";
import { MyFriendPropositionComponent } from "./myfriend-proposition/myfriend-proposition.component";
import {MyFriendHomeComponent} from './myfriend-home/myfriend-home.component';
import { AuthGuard } from '../../../_guards/index';


@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: "MyFriends",
                component: MyFriendHomeComponent,
                canActivate: [AuthGuard],
                children: [
                    {
                        path: "",
                        children: [
                            { path: "list", component: MyFriendListComponent },
                            { path: "proposition", component: MyFriendPropositionComponent },
                            { path: "", redirectTo: "list", pathMatch: "full" }
                        ]
                    }
                ]
            }
        ])
    ],
    exports: [
        RouterModule
    ]
})
export class MymessageRoutingModule { }
