export interface Account {
  accountNumber: string;
  amount: number;
  status: number;
  paymentMethod: number;
  created: string;
  updated: string;
};

export const toAccount = (json: any) : Account => ({
  ...json
});
