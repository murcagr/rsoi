    import configureStore from '../store/configureStore.js';

const updateCatsType = 'UPDATE_CATS';
const decrementCountType = 'DECREMENT_COUNT';
const AddCatType = 'ADD_CAT';
const initialState = { cats: [] };

export const actionCreators = {
    catsRequest: () => (dispatch) => {
        fetch('https://localhost:5049/api/gw/cats')
            .then(res => res.json())
            .then((data) => {
                dispatch(actionCreators.updateCats(data));
                console.log("cats data from action: ", data)
            })
            .catch(console.log)
    },

    updateCats: (cats) => ({ type: updateCatsType, cats: cats }),



    catsAdd: (newCat) => (dispatch) => {
        fetch('https://localhost:5049/api/gw/cats', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'

            },
            body: JSON.stringify({
                name: newCat.name,
                breed: newCat.breed
            })
        })
            .then(res => res.json())
            .catch(console.log)
    },

    catsDelete: (delCat) => (dispatch) => {
        fetch('https://localhost:5049/api/gw/cats/' + delCat.id.id, {
            method: 'DELETE',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'

            },
            
        })
            .then(res => res.json())
            .catch(console.log)
    }

};

export const catsReducer = (state, action) => {
    state = state || initialState;

    if (action.type === updateCatsType) {
        return { cats: action.cats };
    }

    return state;
};

export default catsReducer