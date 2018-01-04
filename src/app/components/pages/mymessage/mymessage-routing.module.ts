import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";

import { MyMessageComponent } from "./mymessage-list/mymessage.component";
import { MymessageDetailsComponent } from "./mymessage-details/mymessage-details.component";
import { AuthGuard } from '../../../_guards/index';


@NgModule({
    imports: [
        RouterModule.forChild([
            { path: "MyMessages", component: MyMessageComponent,canActivate:[AuthGuard] },
            {
                path: "MyMessages/:id",
                component: MymessageDetailsComponent,
                canActivate:[AuthGuard]
            }
        ])
    ],
    exports: [
        RouterModule
    ]
})
export class MymessageRoutingModule { }
