import { useEffect, useState } from 'react';
import './App.css';
import CheeseImageArr from "./CheeseImages";

function App() {
    const [cheeses, setCheeses] = useState();

    // Fetches data using the poplateData function
    useEffect(() => {
        populateData();
    }, []);

    // Updates weight parameter with user's input
    const weightChange = (id, result) => {
        setCheeses((prevCheeses) =>
            prevCheeses.map((cheese) =>
                cheese.index === id ? { ...cheese, weight: result } : cheese
            )
        );
    };

    const contents = cheeses === undefined

        // Displays "Loading..." when fetching data
        ? <p><em>Loading...</em></p>
        : <table className="table table-striped" aria-labelledby="tableLabel">
            <thead>
                <tr>
                    {/* Headings for the table */}
                    <th>Image</th>
                    <th>Name</th>
                    <th>Colour</th>
                    <th>Price per Kg</th>
                    <th>Weight (Kg)</th>
                    <th>Total Price</th>
                </tr>
            </thead>
            <tbody>
                {cheeses.map(cheese => {

                    {/* Matches the Image ID with cheese index */ }
                    const cheeseImage = CheeseImageArr.find(image => image.id === cheese.index);

                    return (

                        // Sets table key as Index from the cheese list
                        <tr key={cheese.index}>
                            <td>

                                {/* Displays correct image for each entry */}
                                {cheeseImage ? (
                                    <img src={cheeseImage.image} alt={cheese.name} width="100" height="90" />
                                ) : (
                                    'No Image'
                                )}
                            </td>

                            {/* Displays correct parameters for each entry */}
                            <td>{cheese.name}</td>
                            <td>{cheese.colour}</td>
                            <td>${cheese.price.toFixed(2)}</td>
                            <td>

                                {/* User can input a number in weight */}
                                <input
                                    type="number"
                                    min="0"
                                    step="0.01"
                                    value={cheese.weight}
                                    onChange={(e) =>
                                        weightChange(cheese.index, parseFloat(e.target.value))
                                    }
                                />
                            </td>

                            {/* Calculates Total Price based on the weight * price per kg */}
                            <td>${(cheese.price * cheese.weight).toFixed(2)}</td>
                        </tr>
                    );
                })}
            </tbody>
        </table>;

    return (
        <div>
            {/* Title and description headings */}
            <h1 id="tableLabel">{"Patient Zero Cheeseria"}</h1>
            <p>{"Have a look at the 5 different cheeses we have available down below!"}</p> <br />
            {contents}
        </div>
    );

    // Function that fetches data
    async function populateData() {
        const response = await fetch('cheese');
        const data = await response.json();

        // Modifies data to include weight parameter, initialised as 0
        const weightData = data.map((cheese) => ({
            ...cheese,
            weight: 0.
        }))

        // Sets variable that contains the cheese list + the new weight parameter
        setCheeses(weightData);
    }
}

export default App;