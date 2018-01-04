import { Component } from '@angular/core';

import './_content/app.less';
import './_content/modal.less';

@Component({
  moduleId: module.id,
  selector: 'app',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'app';
}
