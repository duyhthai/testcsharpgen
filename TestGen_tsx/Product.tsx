class Product {
  protected name: string;

  constructor(name: string) {
    this.name = name;
  }

  getInfo(): string {
    return `Product: ${this.name}`;
  }
}
