export interface Game {
  name: string;
  description: string;
  developer: string;
  publisher: string;
  releaseDates: ReleaseDate[]
}

interface ReleaseDate {
  region: string;
  date: Date;
}
