import React, { Component } from 'react';
import TableRow from './TableRow';
import logo from "./assets/loading.gif";


export class TableProduct extends Component {
    constructor(props) {
        super(props);
        this.state = {
            products: [],
            loading: true
        };

        fetch('api/Docs/GetProducts')
            .then(response => response.json())
            .then(data => {
                this.setState({ products: data, loading: false });
                //console.log(this.state);
            });


    }

    componentDidMount() {
        this.setState({
            isLoading: true
        })
        //fetch('api/Docs/GetProducts')
        //    .then(response => {
        //        if (response.ok) {
        //            return response.json()
        //        } else {
        //            throw new Error('Something went wrong...')
        //        }
        //    })
        //    .then(data => this.setState({
        //        employeeArr: data,
        //        loading: false
        //    }))
        //    .catch(error => this.setState({
        //        error: null,
        //        loading: false
        //    }))


    }

    //downloadDocument(id) {
    //    //this.props.onDelete(id);
    //    console.log('TableProduct - ' + id);
    //    fetch('api/Docs/DownloadDoc?id=' + id)
    //        .then(response => {
    //            if (response.ok) {
    //                console.log(response);
    //                return response.json();
    //            } else {
    //                alert('Something went wrong...');
    //                throw new Error('Something went wrong...');
    //            }
    //        })
    //        .then(data => this.setState({
    //            employeeArr: data,
    //            loading: false
    //        }))
    //        .catch(error => this.setState({
    //            error: null,
    //            loading: false
    //        }));
    //}

    render() {
        let tableRows;
        if (this.state.loading) {
            <p><img src={logo} alt="loading..." /></p> 
        } else {
            if (this.state.products) {
                tableRows = this.state.products.map(product => {
                    return (
                        <TableRow
                            //onDownload={this.downloadDocument.bind(this)}
                            key={product.idProduct}
                            product={product} />
                    )
                });
            }
        }

        return (
            <div className="Projects">
                <h3>Latest Projects</h3>
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
                        {tableRows}
                    </tbody>
                </table>
            </div>
        );
    }
}
