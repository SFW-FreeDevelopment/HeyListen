import {Component, Inject, Input} from '@angular/core';
import {Game} from "../../../models/game";

@Component({
  selector: 'app-game-card-component',
  templateUrl: './game-card.component.html'
})
export class GameCardComponent {
  @Input() game: Game | undefined;
  url: string;

  constructor(@Inject('BASE_URL') baseUrl: string) {
    this.url = `${baseUrl}/games/${this.game?.id ?? ""}`;
  }

  getYear(): string {
    if (!this.game?.releaseDates || this.game?.releaseDates?.length <= 0) {
      return '';
    }
    let date = new Date(this.game.releaseDates[0].date);
    return date.getFullYear().toString();
  }
}
