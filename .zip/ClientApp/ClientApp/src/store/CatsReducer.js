import configureStore from '../store/configureStore.js';

const updateCatsType = 'UPDATE_CATS';
const decrementCountType = 'DECREMENT_COUNT';
const AddCatType = 'ADD_CAT';
const initialState = { cats: [] };

export const actionCreators = {
    catsRequest: () => (dispatch) => {
        fetch('https://localhost:5052/api/cats')
            .then(res => res.json())
            .then((data) => {
                dispatch(actionCreators.updateCats(data));
                console.log("cats data: ", data)
            })
            .catch(console.log)
    },

    updateCats: (cats) => ({ type: updateCatsType, cats: cats }),

    

    catsAdd: (name) => (dispatch) => {
        fetch('https://localhost:5052/api/cats', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'

            },
            body: JSON.stringify({
                name: name
            })
        })
        .then(res => res.json())
        .catch(console.log)
    },

    catsDelete: (id) => (dispatch) => {
        
        fetch('https://localhost:5052/api/cats' + id, {
            method: 'DELETE',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'

            },
            body: JSON.stringify({
                id: id
            })
        })
        .then(res => res.json())
        .catch(console.log)
    }

};

export const reducer = (state, action) => {
    state = state || initialState;

    if (action.type === updateCatsType) {
        return { cats: action.cats };
    }



    return state;
};
