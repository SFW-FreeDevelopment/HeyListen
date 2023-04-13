import {Component, Inject} from '@angular/core';
import {GameService} from "../services/game.service";
import {Game} from "../models/game"

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  public games: Game[] = [];

  constructor(gameService: GameService) {
    gameService.get().subscribe(data => this.games = data)
  }
}
