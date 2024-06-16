
import React, { useState, useEffect } from 'react';

// If you know the structure of your data, define it here
type ImageData = {
  [key: string]: string; // Assuming each key in the data is a string and maps to a string URL
};

const Card: React.FC = () => {
  // Initialize urlArray with an empty array and set its type to string[]
  const [urlArray, setUrlArray] = useState<string[]>([]);

  useEffect(() => {
    const interval = setInterval(() => {
      fetch("http://localhost:5000/api/Users/getImages")
        .then(response => response.json())
        .then(data => {
          // Cast the data to the ImageData type and then get its values
          const imageUrls = Object.values(data as ImageData);
          setUrlArray(imageUrls);
        })
        .catch(error => console.error(error));
    }, 5000); // Fetches data every 5 seconds

    return () => clearInterval(interval); // Cleanup on unmount
  }, []);

  if (urlArray.length === 0) {
    return <div style={{ color: 'white' }}>waiting for data...</div>;
  }

  // Removed the standalone map that was logging to the console

  return (
    <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fill, minmax(250px, 1fr))', gridAutoRows: '15fr', gridGap: '10px', width: '100%' }}>
      {urlArray.map((url, index) => (
        <div key={index}
             className="max-w-sm rounded overflow-hidden shadow-lg"
             style={{ transition: 'transform .2s' }} // Smooth transition
             onMouseOver={event => (event.currentTarget.style.transform = 'scale(1.05)')} // Scale up on hover
             onMouseOut={event => (event.currentTarget.style.transform = 'scale(1)')} // Scale down when not hovered
        >
          <img className="w-full" src={'http://localhost:5000' + url.substring(7)} alt="Sunset in the mountains" style={{ width: '400px', height: '400px', objectFit: 'cover' }} />
          <div className="px-6 py-4">
            <div className="font-bold text-xl mb-2 text-amber-600">The Coldest Sunset</div>
            <p className="text-white text-base">
              Lorem ipsum dolor sit amet, consectetur adipisicing elit. Voluptatibus quia, nulla! Maiores
              et perferendis eaque, exercitationem praesentium nihil.
            </p>
          </div>
          <div className="px-6 pt-4 pb-2">
            <span className="inline-block bg-gray-200 rounded-full px-3 py-1 text-sm font-semibold text-gray-700 mr-2 mb-2">#photography</span>
            <span className="inline-block bg-gray-200 rounded-full px-3 py-1 text-sm font-semibold text-gray-700 mr-2 mb-2">#travel</span>
            <span className="inline-block bg-gray-200 rounded-full px-3 py-1 text-sm font-semibold text-gray-700 mr-2 mb-2">#winter</span>
          </div>
        </div>
      ))}
    </div>
  );
};

export default Card;