import React, { Component } from 'react';
import toastr from 'reactjs-toastr';
import 'reactjs-toastr/lib/toast.css';


class TableRow extends Component {
    downloadDocument(id) {
        //this.props.onDownload(id);
        console.log('TableProduct - ' + id);
        var name = id;
        fetch('api/Docs/DownloadDoc?id=' + id)
            .then(response => {
                if (response.ok) {
                    console.log(response);
                    return response.blob();
                } else {
                    //toastr.success('Success Message', 'Title', { displayDuration: 3000 });
                    console.log(response);
                    alert('Something went wrong...');
                    throw new Error('Something went wrong...');
                }
            })
            .then(data => {
                if (navigator.msSaveBlob) { // For ie and Edge
                    return navigator.msSaveBlob(data, name);
                }
                else {
                    let link = document.createElement('a');
                    link.href = window.URL.createObjectURL(data);
                    link.download = name;
                    document.body.appendChild(link);
                    link.dispatchEvent(new MouseEvent('click', { bubbles: true, cancelable: true, view: window }));
                    link.remove();
                    window.URL.revokeObjectURL(link.href);
                }
            })
            .catch(error => this.setState({
                error: null,
                loading: false
            }));

    }


    render() {
        return (
            <tr className="Project">
                <td>{this.props.product.productName}</td>
                <td>{this.props.product.supplierName}</td>
                <td>
                    <button
                        className="btn btn-default btn-md"
                        onClick={this.downloadDocument.bind(this, this.props.product.idProduct)}
                    >
                        Download
                    </button>
                </td>
                <td>{this.props.product.type}</td>
                <td>{this.props.product.downloaded}</td>
            </tr>
        );
    }
}


export default TableRow;