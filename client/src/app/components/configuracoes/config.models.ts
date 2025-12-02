export interface DetalhesConfigModel {
  id: string;
  gasolina: number;
  gas: number;
  diesel: number;
  alcool: number;
}

export interface EditarConfigModel {
  gasolina: number;
  gas: number;
  diesel: number;
  alcool: number;
}

export interface EditarConfigResponseModel {
  id: string;
  gasolina: number;
  gas: number;
  diesel: number;
  alcool: number;
}

