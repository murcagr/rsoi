import React, { Component } from 'react';
import { actionCreators as catsActions } from '../reducers/Cats';
import { actionCreators as ownersActions } from '../reducers/Owners';
import { actionCreators as foodsActions } from '../reducers/Foods';
import { actionCreators as cotsActions } from '../reducers/COF';
import { Redirect } from 'react-router-dom'
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import ReactPaginate from 'react-paginate';
import CatList from './CatList';
import CatsTable from './CatsTableC'

class CatsPage extends Component {

    constructor(props) {
        super(props);

        this.state = {
            cats: [],
            foods: [],
            owners: [],
            page: 0,
            offset: 0,
            newCatData: {
                name: '',
                breed: '',
                foodId: ''
            },
            delCatData: {
                id : ''
            },
            delOwnerCatData: {
                id: ''
            },
            newOwnerData: {
                name: '',
                age: '',
                city: ''
            },
            newCatOwnerData: {
                nameCat: '',
                breed: '',
                foodId: '',
                nameOwner: '',
                age: '',
                city: '',
            }
        };

        this.handleSubmit = this.handleSubmit.bind(this);
        this.handleSubmit2 = this.handleSubmit2.bind(this);
        this.handleSubmit2 = this.handleSubmit2.bind(this);
        this.handleSubmit2Change = this.handleSubmit2Change.bind(this);
        this.handleDelete = this.handleDelete.bind(this);
        this.handleDel2 = this.handleDel2.bind(this);
        this.handleChange = this.handleChange.bind(this);
        this.handleDelChange = this.handleDelChange.bind(this);
        this.handleDel2Change = this.handleDel2Change.bind(this);
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

    handleDel2(event) {
        event.preventDefault();
        let delOwnerCatData = {
            id: this.state.delOwnerCatData.id
        }
        this.props.actions.cofsDelete(delOwnerCatData);
        this.props.actions.cotsRequest(this.state.page);
    }
    handleDel2Change(event) {
        const field = event.target.name;
        const delOwnerCatData = this.state.delOwnerCatData;
        delOwnerCatData[field] = event.target.value;
        return this.setState({ delOwnerCatData: delOwnerCatData});
    }

    handleSubmit(event) {
        event.preventDefault();
        let newCatData = {
            name: this.state.newCatData.name,
            breed: this.state.newCatData.breed
        }
        this.props.actions.catsAdd(newCatData);
        this.props.actions.cotsRequest(this.state.page);
    }

    handleSubmit2Change(event) {
        const field = event.target.name;
        const newCatOwnerData = this.state.newCatOwnerData;
        newCatOwnerData[field] = event.target.value;
        return this.setState({ newCatOwnerData: newCatOwnerData });
    }

    handleSubmit2(event) {
        event.preventDefault();
        let newCatOwnerData = {
            nameCat: this.state.newCatOwnerData.nameCat,
            breed: this.state.newCatOwnerData.breed,
            nameOwner: this.state.newOwnerData.nameOwner,
            age: this.state.newCatOwnerData.age,
            city: this.state.newCatOwnerData.city
        }
        this.props.actions.cofAdd(newCatOwnerData);
        this.props.actions.cotsRequest(this.state.page);
    }

    handleDelete(event) {
        event.preventDefault();
        let delCatData = {
            id: this.state.delCatData,
        }
        this.props.actions.catsDelete(delCatData);
        this.props.actions.cotsRequest(this.state.page);
    }

    componentDidMount() {
        this.props.actions.cotsRequest(this.state.page);
    }

    //  if (!localStorage.token) {
    //return<Redirect to = "/login" />
      //  }
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
                            <label ><b>Add cat</b></label><br />
                            <div class="form-group">
                                <label >Name of cat</label>
                                <input class="form-control" id="examplename" placeholder="Name"
                                    name = "name"
                                    value={this.state.newCatData.name} onChange={this.handleChange} required
                                />
                            </div>
                            <div class="form-group">
                                <label>Breed of the cat</label>
                                <input type="breed" class="form-control" id="examplebreed" name = "breed"
                                    value={this.state.newCatData.breed} onChange={this.handleChange} required
                                    placeholder="Breed"
                                />
                            </div>
                            <div class="form-group">
                                <label>Food</label>
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
                        <CatsTable />
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
                <div class="row">
                    <div class="row justify-content-center" >
                        <div class="col">
                            <form onSubmit={this.handleSubmit2}>
                                <div class="form-group">
                                    <label ><b>Add cat with owner</b></label><br/>
                                    <label > Name of cat </label>
                                    <input class="form-control" id="examplename" placeholder="Name"
                                        name="nameCat"
                                        value={this.state.newCatOwnerData.nameCat} onChange={this.handleSubmit2Change} required
                                    />
                                </div>
                                <div class="form-group">
                                    <label for="exampleInputPassword1">Breed of the cat</label>
                                    <input type="breed" class="form-control" id="examplebreed" name="breed"
                                        value={this.state.newCatOwnerData.breed} onChange={this.handleSubmit2Change} required
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
                                <div class="form-group">
                                    <label >Name of Owner</label>
                                    <input class="form-control" id="examplename" placeholder="Name"
                                        name="nameOwner"
                                        value={this.state.newCatOwnerData.nameOwner} onChange={this.handleSubmit2Change} required
                                    />
                                </div>
                                <div class="form-group">
                                    <label for="exampleInputPassword1">Breed of the cat</label>
                                    <input class="form-control" id="examplebreed" name="city"
                                        value={this.state.newCatOwnerData.city} onChange={this.handleSubmit2Change} required
                                        placeholder="City"
                                    />
                                </div>
                                <div class="form-group">
                                    <label >Age of owner</label>
                                    <input type="number" id="replyNumber" min="0" max="99"
                                        name="age" value={this.state.newCatOwnerData.age} onChange={this.handleSubmit2Change} required
                                        />
                                </div>
                                <button type="submit" class="btn btn-primary">Add</button>

                            </form>
                        </div>
                        <div class="col">
                        </div>
                        <div class="col">
                            <form onSubmit={this.handleDel2}>
                                <label >Delete owner and his(her) cats</label>
                                <div class="form-group">
                                    <label >Id of the owner</label>
                                    <input class="form-control" id="examplename" placeholder="Name"
                                        name="id"
                                        value={this.state.delOwnerCatData.id} onChange={this.handleDel2Change} required
                                    />
                                </div>
                                <button type="submit" class="btn btn-primary">Add</button>
                            </form>
                        </div>
                        

                    </div>
                </div> 
            </div>
        );
    }

   // render() {

     //   return (this.props.cats.length) ? this.renderPage() : (
      //      <span>Loading wells...</span>
       // )
   // }
}



function mapDispatchToProps(dispatch) {
    return {
        actions: bindActionCreators(Object.assign(catsActions, ownersActions, foodsActions, cotsActions), dispatch)
    };
}

function mapStateToProps(state) {
    return {
        cats: state.cats.cats,
        owners: state.owners.owners,
        foods: state.foods.foods,
        cots: state.cots.cots
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(CatsPage);