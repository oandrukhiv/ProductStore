import React, { Component } from 'react';

export class Order extends Component {
    static displayName = Order.name;

    constructor(props) {
        super(props);
        this.state = {
            orders: [],
            isLoaded: false,
        };
    }

    componentDidMount() {
        fetch('https://localhost:44304/api/orders')
            .then(res => res.json())
            .then(json => {
                this.setState({
                    isLoaded: true,
                    items: json,
                })
            });
    }

    render() {
        var { isLoaded, items } = this.state;

        if (!isLoaded) {
            return <div>Loading...</div>
        }
        else {
            return (
                <table className='table table-striped' aria-labelledby="tabelLabel">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Creation Date</th>
                            <th>Details</th>
                            <th>Total Price</th>
                            <th>Is Paid</th>
                            <th>Created By</th>
                            <th>Destination Point</th>
                        </tr>
                    </thead>
                    <tbody>
                        {items.map(item =>
                            <tr key={item.id}>
                                <td>{item.id}</td>
                                <td>{item.creationDate}</td>
                                <td>{item.additionalDetails}</td>
                                <td>{item.totalPrice}</td>
                                <td>{item.isPaid}</td>
                                <td>{item.createdBy}</td>
                                <td>{item.destinationPoint}</td>
                            </tr>
                        )}
                    </tbody>
                </table>
            );
        }
    }

}
