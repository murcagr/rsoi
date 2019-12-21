import React, { Component} from 'react';
import PropTypes from 'prop-types';
import { actionCreators as catsActions } from '../reducers/Cats';
import { actionCreators as ownersActions } from '../reducers/Owners';
import { actionCreators as foodsActions } from '../reducers/Foods';
import { actionCreators as cotsActions } from '../reducers/COF';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { makeStyles, useTheme } from '@material-ui/core/styles';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableFooter from '@material-ui/core/TableFooter';
import TablePagination from '@material-ui/core/TablePagination';
import TableRow from '@material-ui/core/TableRow';
import Paper from '@material-ui/core/Paper';
import IconButton from '@material-ui/core/IconButton';
import FirstPageIcon from '@material-ui/icons/FirstPage';
import KeyboardArrowLeft from '@material-ui/icons/KeyboardArrowLeft';
import KeyboardArrowRight from '@material-ui/icons/KeyboardArrowRight';
import LastPageIcon from '@material-ui/icons/LastPage';

function useStyles1() {
    return makeStyles(theme => ({
        root: {
            flexShrink: 0,
            marginLeft: theme.spacing(2.5),
        },
    }));
}

function useStyles2() {
    return makeStyles({
        root: {
            width: '100%',
        },
        table: {
            minWidth: 500,
        },
        tableWrapper: {
            overflowX: 'auto',
        },
    });
}




class CatsTable extends Component {
    constructor(props) {
        super(props);
        console.log(this.props)
        this.state = {
            cats: [],
            foods: [],
            owners: [],
            page: 0,
            rowsPerPage: 10
        }

        this.handleChangePage = this.handleChangePage.bind(this);
        
    };

    async componentDidMount() {
        await this.props.actions.cotsRequest(this.state.page);
    }

    TablePaginationActions(props) {
    const classes = useStyles1();
    const theme = useTheme();

    const { count, page, rowsPerPage, onChangePage } = props;

    const handleFirstPageButtonClick = event => {
        onChangePage(event, 0);
    };

    const handleBackButtonClick = event => {
        onChangePage(event, page - 1);
    };

    const handleNextButtonClick = event => {
        onChangePage(event, page + 1);
    };

    const handleLastPageButtonClick = event => {
        onChangePage(event, Math.max(0, Math.ceil(count / rowsPerPage) - 1));
    };

    return (
        <div className={classes.root}>
            <IconButton
                onClick={handleFirstPageButtonClick}
                disabled={page === 0}
                aria-label="first page"
            >
                {theme.direction === 'rtl' ? <LastPageIcon /> : <FirstPageIcon />}
            </IconButton>
            <IconButton onClick={handleBackButtonClick} disabled={page === 0} aria-label="previous page">
                {theme.direction === 'rtl' ? <KeyboardArrowRight /> : <KeyboardArrowLeft />}
            </IconButton>
            <IconButton
                onClick={handleNextButtonClick}
                disabled={page >= Math.ceil(count / rowsPerPage) - 1}
                aria-label="next page"
            >
                {theme.direction === 'rtl' ? <KeyboardArrowLeft /> : <KeyboardArrowRight />}
            </IconButton>
            <IconButton
                onClick={handleLastPageButtonClick}
                disabled={page >= Math.ceil(count / rowsPerPage) - 1}
                aria-label="last page"
            >
                {theme.direction === 'rtl' ? <FirstPageIcon /> : <LastPageIcon />}
            </IconButton>
        </div>
    );
}

    handleChangePage = (event, newPage) => {

        setTimeout(this.props.actions.cotsRequest, 2000, this.state.page);
        this.setState({ page: newPage });
        console.log(this.state.page)
    };

    render() {
        
        const classes = useStyles2();

        if (this.props.cots == undefined) {
            return <div/>
        }
        const emptyRows = this.state.rowsPerPage -  this.props.cots.length;

        return (
            <Paper className={classes.root}>
                <div className={classes.tableWrapper}>
                    <Table className={classes.table} aria-label="custom pagination table">
                        <TableBody>
                            {(this.state.rowsPerPage > 0
                                ? this.props.cots.slice(0, this.state.rowsPerPage)
                                : this.props.cots
                            ).map(row => (
                                <TableRow key={row.id}>
                                    <TableCell component="th" scope="row">
                                        {row.cat.id}<br />
                                        {row.cat.name}<br />
                                        {row.cat.breed}
                                    </TableCell>

                                    <TableCell align="right">{row.owner != null ? row.owner.id : ""}<br />
                                        {row.owner != null ? row.owner.name : "No owner"}<br />
                                        {row.owner != null ? row.owner.city : ""}<br />
                                        {row.owner != null ? row.owner.age : ""}
                                    </TableCell>
                                    <TableCell align="right">{row.food != null ? row.food.id : ""}<br />
                                        {row.food != null ? row.food.name : "No food"}<br />
                                        {row.food != null ? row.food.producer : ""}<br />
                                        {row.food != null ? row.food.doze : ""}<br />
                                    </TableCell>
                                </TableRow>
                            ))}

                            {emptyRows > 0 && (
                                <TableRow style={{ height: 53 * emptyRows }}>
                                    <TableCell colSpan={6} />
                                </TableRow>
                            )}
                        </TableBody>
                        <TableFooter>
                            <TableRow>
                                <TablePagination
                                    rowsPerPageOptions={[10]}
                                    colSpan={3}
                                    count={this.props.cots.length+100}
                                    rowsPerPage={this.state.rowsPerPage}
                                    page={this.state.page}
                                    SelectProps={{
                                        inputProps: { 'aria-label': 'rows per page' },
                                        native: true,
                                    }}
                                    onChangePage={this.handleChangePage}
                                    ActionsComponent={this.TablePaginationActions}
                                />
                            </TableRow>
                        </TableFooter>
                    </Table>
                </div>
            </Paper>
        );
    }
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

export default connect(mapStateToProps, mapDispatchToProps)(CatsTable);