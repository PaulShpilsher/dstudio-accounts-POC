import {LookupType} from './lookup.model';

export const getAccountStatusLookup = (): LookupType => ({
  1: "Новый",
  2: "Оплачен",
  3: "Отменен"
});
