    import configureStore from '../store/configureStore.js';

const updateOwnersType = 'UPDATE_OWNERS';
const decrementCountType = 'DECREMENT_COUNT';
const AddOwnerType = 'ADD_OWNER';
const initialState = { owners: [] };

export const actionCreators = {
    ownersRequest: () => (dispatch) => {
        fetch('https://localhost:5049/api/gw/owners')
            .then(res => res.json())
            .then((data) => {
                dispatch(actionCreators.updateOwners(data));
                console.log("owners data: ", data)
            })
            .catch(console.log)
    },

    updateOwners: (owners) => ({ type: updateOwnersType, owners: owners }),



    ownersAdd: (newOwner) => (dispatch) => {
        fetch('https://localhost:5049/api/gw/owners', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'

            },
            body: JSON.stringify({
                name: newOwner.name,
                breed: newOwner.breed
            })
        })
            .then(res => res.json())
            .catch(console.log)
    },

    ownersDelete: (delOwner) => (dispatch) => {
        fetch('https://localhost:5049/api/gw/owners/' + delOwner.id.id, {
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

export const ownersReducer = (state, action) => {
    state = state || initialState;

    if (action.type === updateOwnersType) {
        return { owners: action.owners };
    }

    return state;
};

export default ownersReducer