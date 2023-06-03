import { User } from "./user";

export class Statistics {
  owner!: User;
  totalItems!: number;
  goldSpent!: number;
  completedQuestsCounter!: number;
  totalKilled!: number;
  killedPlayersCounter!: number;
  killedNpcCounter!: number;
  killedAnimalsCounter!: number;
  totalDeath!: number;
  deathFromPlayersCounter!: number;
  deathFromNpcCounter!: number;
  deathFromAnimalsCounter!: number;
}
