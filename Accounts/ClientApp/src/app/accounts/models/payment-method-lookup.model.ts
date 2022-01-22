import {LookupType} from './lookup.model';

export const getPaymentMethodLookup = (): LookupType => ({
  1: "Кредитная карта",
  2: "Дебетовая карта",
  3: "Электронный чек"
});
