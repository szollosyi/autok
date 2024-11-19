import React, { useEffect, useState } from 'react';
import axios from 'axios';

function CarList() {
  const [cars, setCars] = useState([]);
  const [loading, setLoading] = useState(true);
  const [selectedCar, setSelectedCar] = useState(null);
  const [error, setError] = useState(null);

  useEffect(() => {
    fetchCars();
  }, []);

  const fetchCars = () => {
    axios.get('https://cars.sulla.hu/cars')
      .then(response => {
        setCars(response.data);
        setLoading(false);
      })
      .catch(error => {
        console.error("Hiba történt az adatok lekérésekor:", error);
        setError("Nem sikerült lekérni az adatokat.");
        setLoading(false);
      });
  };

  const fetchCarById = (id) => {
    axios.get(`https://cars.sulla.hu/cars/${id}`)
      .then(response => {
        setSelectedCar(response.data);
      })
      .catch(error => {
        console.error(`Hiba történt az autó lekérésekor ID alapján (ID: ${id}):`, error);
        setError(`Nem sikerült lekérni az autót ID alapján.`);
      });
  };

  const updateCar = (id, updatedData) => {
    axios.put(`https://cars.sulla.hu/cars/${id}`, updatedData)
      .then(response => {
        console.log("Autó frissítve:", response.data);
        fetchCars();
      })
      .catch(error => {
        console.error(`Hiba történt az autó frissítésekor (ID: ${id}):`, error);
        setError(`Nem sikerült frissíteni az autót.`);
      });
  };

  if (loading) {
    return <p>Adatok betöltése...</p>;
  }

  return (
    <div>
      <h1>Autók listája</h1>
      {error && <p style={{ color: 'red' }}>{error}</p>}
      <div style={{ display: 'flex', flexWrap: 'wrap' }}>
        {cars.map((car) => (
          <div key={car.id} style={{ border: '1px solid #ccc', margin: '10px', padding: '10px', width: '200px' }}>
            <h2>{car.car_name}</h2>
            <p>Szín: {car.car_color}</p>
            <img
              src={`data:image/jpeg;base64,${car.car_picture}`}
              alt={car.car_name}
              style={{ width: '100%', height: 'auto' }}
            />
            <button onClick={() => fetchCarById(car.id)}>Részletek</button>
            <button onClick={() => updateCar(car.id, { car_name: "Új Név", car_color: "Új Szín" })}>Frissítés</button>
          </div>
        ))}
      </div>

      {selectedCar && (
        <div style={{ marginTop: '20px', padding: '10px', border: '1px solid #ccc' }}>
          <h2>Autó Részletei</h2>
          <p>ID: {selectedCar.id}</p>
          <p>Név: {selectedCar.car_name}</p>
          <p>Szín: {selectedCar.car_color}</p>
          <img
            src={`data:image/jpeg;base64,${selectedCar.car_picture}`}
            alt={selectedCar.car_name}
            style={{ width: '100%', height: 'auto' }}
          />
        </div>
      )}
    </div>
  );
}

export default CarList;
