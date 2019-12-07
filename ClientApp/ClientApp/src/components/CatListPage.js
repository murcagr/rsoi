import React, { Component } from 'react';
import { actionCreators as catsActions } from '../reducers/Cats';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import ReactPaginate from 'react-paginate';
import CatList  from './CatList';

class CatsPage extends Component {

    constructor(props) {
        super(props);

        this.state = {
            data: [],
            offset: 0,
        };
    }

    componentDidMount() {
        this.props.actions.catsRequest();
    }

    handlePageClick = data => {
        let selected = data.selected;
        let offset = Math.ceil(selected * this.props.perPage);

        this.setState({ offset: offset }, () => {
            this.props.actions.catsRequest();
        });
    };

    render() {
        return (
            <div className="commentBox">
                <b> {this.props.cats.length} </b>
                <CatList data={this.props.cats} />
                <ReactPaginate
                    previousLabel={'previous'}
                    nextLabel={'next'}
                    breakLabel={'...'}
                    breakClassName={'break-me'}
                    pageCount={this.state.pageCount}
                    marginPagesDisplayed={2}
                    pageRangeDisplayed={5}
                    onPageChange={this.handlePageClick}
                    containerClassName={'pagination'}
                    subContainerClassName={'pages pagination'}
                    activeClassName={'active'}
                />
            {this.props.cats.length != 0 ? this.props.cats[0].name : ""}
            <button onClick={() => this.props.actions.catsAdd("LOSHARA")}>
                    POST
            </button>
            </div>

        );
    }
}



function mapDispatchToProps(dispatch) {
    return {
        actions: bindActionCreators(catsActions, dispatch)
    };
}

function mapStateToProps(state) {
    return {    
        cats: state.cats.cats
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(CatsPage);