import React, { Component } from 'react';


class TableRow extends Component {
    downloadDocument(id) {
        //this.props.onDownload(id);
        console.log('TableProduct - ' + id);
        fetch('api/Docs/DownloadDoc?id=' + id)
            .then(processStatus)
            .then(parseBlob);
    }

    function processStatus(response) {// process status
    debugger
    if (response.status === 200 || response.status === 0) {
        return Promise.resolve(response)
    } else {
        return Promise.reject(new Error('Error loading: ' + url))
    }
}

function parseBlob(response) {
    debugger
    return response.blob();

}
render() {
    return (
        <tr className="Project">
            <td>{this.props.product.productName}</td>
            <td>{this.props.product.supplierName}</td>
            <td>
                <button
                    className="btn btn-default btn-md"
                    onClick={this.downloadDocument.bind(this, this.props.product.idProduct)}>
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
