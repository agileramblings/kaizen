import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';

import { RetrospectiveDetails } from '../_models/retrospective';
import { Like, Dislike, ActionItem } from '../_models/retrospectiveitems';
import { RETROSPECTIVES } from '../app/mock-retrospective';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root',
})
export class RetrospectiveService {

  private kaizenServiceUrl = 'https://localhost:5001/api/v1/retrospectives/';  // URL to web api
  defaultValue: (string) = '"Please start typing"';
  constructor(private http: HttpClient) { }

  getRetrospectives(): Observable<RetrospectiveDetails[]> {
    const response = this.http.get<RetrospectiveDetails[]>(this.kaizenServiceUrl);
    return response;
    // return of(RETROSPECTIVES);
  }

  addLike(retrospectiveId: string, participantId: string): Observable<RetrospectiveDetails> {
    const response = this.http.post<RetrospectiveDetails>(
      this.kaizenServiceUrl + `${retrospectiveId}/like/${participantId}`,
      this.defaultValue,
      httpOptions);
    return response;
  }
  addDislike(retrospectiveId: string, participantId: string): Observable<RetrospectiveDetails> {
    const response = this.http.post<RetrospectiveDetails>(
      this.kaizenServiceUrl + `${retrospectiveId}/dislike/${participantId}`,
      this.defaultValue,
      httpOptions);
    return response;
  }
  addActionItem(retrospectiveId: string, participantId: string): Observable<RetrospectiveDetails> {
    const response = this.http.post<RetrospectiveDetails>(
      this.kaizenServiceUrl + `${retrospectiveId}/actionitem/${participantId}`,
      this.defaultValue,
      httpOptions);
    return response;
  }
}
