import {HttpClient} from "@angular/common/http";
import {Inject, Injectable} from "@angular/core";
import {Game} from "../models/game"
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root',
})
export class GameService {
  private readonly baseUrl: string = '';
  private client: HttpClient;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.client = http;
    this.baseUrl = baseUrl;
  }

  get(): Observable<Game[]> {
    return this.client.get<Game[]>(this.baseUrl + 'games');
  }
}
