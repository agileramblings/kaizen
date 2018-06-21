import { Component, OnInit } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatCardModule, MatIconModule, MatToolbarModule, MatButtonModule, MatFormFieldModule, MatInputModule } from '@angular/material';
import { MatGridListModule } from '@angular/material/grid-list';
import { FlexLayoutModule } from '@angular/flex-layout';

import { RetrospectiveDetails } from '../../_models/retrospective';
import { RetrospectiveService } from '../../_services/retrospective.service';

import { Observable, of } from 'rxjs';

@Component({
  selector: 'app-retrospective',
  templateUrl: './retrospective.component.html',
  styleUrls: ['./retrospective.component.css']
})
export class RetrospectiveComponent implements OnInit {

  retrospectives: RetrospectiveDetails[];
  retrospective: RetrospectiveDetails;

  constructor(private retroService: RetrospectiveService) { }

  ngOnInit() {
    this.getRetros();
  }

  getRetros(): void {
    this.retrospectives = this.retroService.getRetrospectives();
    this.retrospective = this.retrospectives[0];
  }
}
