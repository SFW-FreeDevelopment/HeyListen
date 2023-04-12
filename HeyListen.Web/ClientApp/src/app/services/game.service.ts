import {HttpClient} from "@angular/common/http";
import {Inject, Injectable} from "@angular/core";
import {Game} from "../models/game"

@Injectable({
  providedIn: 'root',
})
export class GameService {
  private games: Game[] = [];
  private baseUrl: string = '';
  private client: HttpClient;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.client = http;
    this.baseUrl = baseUrl;
  }

  get(): Game[] {
    this.client.get<Game[]>(this.baseUrl + 'games').subscribe(result => {
      this.games = result;
    }, error => console.error(error));
    return this.games;
  }
}
