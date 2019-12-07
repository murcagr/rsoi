import React, { Component } from 'react';
import { actionCreators as catsActions } from '../reducers/Cats';
import { actionCreators as ownersActions } from '../reducers/Owners';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import ReactPaginate from 'react-paginate';
import CatList from './CatList';
import CatsTable from './CatsTable'

class CatsPage extends Component {

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
                id : ''
            }
        };

        this.handleSubmit = this.handleSubmit.bind(this);
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

        this.props.actions.ownersRequest();
    }


    render() {
        return (
            <div class="container">
                {console.log(this.props.owners)}
                <div class="row justify-content-center" >
                    <div class="col"/>
                    <div class="col align-self-center">
                        <h1> List of all cats </h1>
                    </div>
                    <div class="col" />
                </div>
                <div class="row">
                    <div class="col">
                        <form onSubmit={this.handleSubmit}>
                            <div class="form-group">
                                <label >Name of cat</label>
                                <input class="form-control" id="examplename" placeholder="Name"
                                    name = "name"
                                    value={this.state.newCatData.name} onChange={this.handleChange} required
                                />
                            </div>
                            <div class="form-group">
                                <label for="exampleInputPassword1">Breed of the cat</label>
                                <input type="breed" class="form-control" id="examplebreed" name = "breed"
                                    value={this.state.newCatData.breed} onChange={this.handleChange} required
                                    placeholder="Breed"
                                />
                            </div>
                            <div class="form-group">
                                <label for="exampleFormControlSelect1">Example select</label>
                                <select class="form-control" id="exampleFormControlSelect1">
                                    <option>1</option>
                                    <option>2</option>
                                    <option>3</option>
                                    <option>4</option>
                                    <option>5</option>
                                </select>
                            </div>
                            <button type="submit" class="btn btn-primary">Add</button>
                        </form>
                            
                    </div>
                    <div class="col">
                        <CatsTable data={this.props.cats} />
                    </div>
                    <div class="col">
                        <form onSubmit={this.handleDelete}>
                            <div class="form-group">
                                <label >Id of the cat</label>
                                <input class="form-control" id="examplename" placeholder="Name"
                                    name="id"
                                    value={this.state.delCatData.id} onChange={this.handleDelChange} required
                                />
                            </div>
                            <button type="submit" class="btn btn-primary">Add</button>
                        </form>
                    </div>
                </div>
            </div>
        );
    }
}



function mapDispatchToProps(dispatch) {
    return {
        actions: bindActionCreators(Object.assign(catsActions, ownersActions), dispatch)
    };
}

function mapStateToProps(state) {
    return {
        cats: state.cats.cats,
        owners: state.owners.owners
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(CatsPage);