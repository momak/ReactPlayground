import React, { Component } from 'react';

export class FetchData extends Component {
    displayName = FetchData.name

    constructor(props) {
        super(props);
        this.state = { products: [], loading: true };

        fetch('api/Docs/GetProducts')
            .then(response => response.json())
            .then(data => {
                this.setState({ products: data, loading: false });
            });
    }

    static renderProductsTable(products) {
        return (
            <table className='table'>
                <thead>
                    <tr>
                        <th>Product Name</th>
                        <th>Supplier Name</th>
                        <th>Document</th>
                        <th>Type</th>
                        <th>Visited</th>
                    </tr>
                </thead>
                <tbody>
                    {products.map(product =>
                        <tr key={product.idProduct}>
                            <td>{product.productName}</td>
                            <td>{product.supplierName}</td>
                            <td>
                                <a className="btn btn-link btn-md" href={'api/Docs/DownloadDoc?id=' + product.idProduct}>download </a></td>
                            <td>{product.type}</td>
                            <td>{product.downloaded}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : FetchData.renderProductsTable(this.state.products);

        return (
            <div>
                <h1>Products Documents</h1>
                <p>List of products safety data sheets.</p>
                {contents}
            </div>
        );
    }
}
