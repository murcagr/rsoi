import React, { Component } from 'react';
import { actionCreators as catsActions } from '../reducers/Cats';
import { actionCreators as ownersActions } from '../reducers/Owners';
import { actionCreators as ownerCatsActions } from '../reducers/OwnerCats';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import ReactPaginate from 'react-paginate';
import CatList from './CatList';
import CatsTable from './CatsTable'

class OwnerCatsPage extends Component {

    constructor(props) {
        super(props);

        this.state = {
            data: [],
            offset: 0,
            newCatData: {
                name: '',
                breed: '',
                foodId: ''
            },
            delCatData: {
                id: ''
            },
            currentUser: {
                name: '',
                age: '',
                city: '',
            },
            currentUserReq: {
                id : '',
            }
        };

        this.handleSubmit = this.handleSubmit.bind(this);
        this.handleGet = this.handleGet.bind(this);
        this.handleGetChange = this.handleGetChange.bind(this);
        this.handleDelete = this.handleDelete.bind(this);
        this.handleChange = this.handleChange.bind(this);
        this.handleDelChange = this.handleDelChange.bind(this);
        this.alertClosed = this.alertClosed.bind(this);
    }


    alertClosed(event) {
        this.props.reg_conf();
    }

    handleChange(event) {
        const field = event.target.name;
        const newCatData = this.state.newCatData;
        newCatData[field] = event.target.value;
        return this.setState({ newCatData: newCatData });
    }

    handleGetChange(event) {
        const field = event.target.name;
        const currentUserReq = this.state.currentUserReq;
        currentUserReq[field] = event.target.value;
        return this.setState({ currentUserReq: currentUserReq});
    }

    handleDelChange(event) {
        const field = event.target.name;
        const delCatData = this.state.delCatData;
        delCatData[field] = event.target.value;
        return this.setState({ delCatData: delCatData });
    }

    handleSubmit(event) {
        event.preventDefault();
        let newCatData = {
            name: this.state.newCatData.name,
            breed: this.state.newCatData.breed
        }
        this.props.actions.catsAdd(newCatData);
        this.props.actions.catsRequest();
    }

    handleGet(event) {
        event.preventDefault();
        let currentUserReq = {
            id: this.state.currentUserReq.id,
        }
        this.props.actions.ownerCatsGet(currentUserReq);
    }


    handleDelete(event) {
        event.preventDefault();
        let delCatData = {
            id: this.state.delCatData,
        }
        this.props.actions.catsDelete(delCatData);
        this.props.actions.catsRequest();
    }

    componentDidMount() {
        this.props.actions.catsRequest();
    }


    render() {
        return (
            <div class="container">
                <div class="row justify-content-center" >
                    <div class="col">
                        <p class="text-left">Id {this.props.owners.length} </p>
                        <form onSubmit={this.handleGet}>
                            <div class="form-group">
                                <label >Id of the owner</label>
                                <input class="form-control" id="examplename" placeholder="Name"
                                    name="id"
                                    value={this.state.currentUserReq.id} onChange={this.handleGetChange} required
                                />
                            </div>
                            <button type="submit" class="btn btn-primary">Add</button>
                        </form>
                    </div>
                    <div class="col align-self-center">
                        <h1> List of all cats </h1>
                    </div>
                    <div class="col"/>
                </div>
                <div class="row">
                    <div class="col">
                    </div>
                    <div class="col">
                        <CatsTable data={this.props.cats} />
                    </div>
                    <div class="col">
                        
                    </div>
                </div>
            </div>
        );
    }
}



function mapDispatchToProps(dispatch) {
    return {
        actions: bindActionCreators(Object.assign(catsActions, ownersActions, ownerCatsActions), dispatch)
    };
}

function mapStateToProps(state) {
    return {
        cats: state.cats.cats,
        owners: state.owners.owners,
        ownerCats: state.ownerCats.ownerCats
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(OwnerCatsPage);