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
  participantId: (string) = 'DavidChen';


  constructor(private retroService: RetrospectiveService) { }

  ngOnInit() {
    this.getRetros();
  }

  getRetros(): void {
    const response = this.retroService.getRetrospectives();
    response.subscribe(retrospectives => {
        console.log(retrospectives);
        this.retrospectives = retrospectives;
        this.retrospective = retrospectives[0];
      });
  }

  addLike(): void {
    const response = this.retroService.addLike(this.retrospective.id, this.participantId);
    response.subscribe(retrospective => {
      this.retrospective = retrospective;
    });
  }
  addDislike(): void {
    const response = this.retroService.addDislike(this.retrospective.id, this.participantId);
    response.subscribe(retrospective => {
      this.retrospective = retrospective;
    });
  }
  addActionItem(): void {
    const response = this.retroService.addActionItem(this.retrospective.id, this.participantId);
    response.subscribe(retrospective => {
      this.retrospective = retrospective;
    });
  }
}
