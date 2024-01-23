import React from "react";
import { Line } from "react-chartjs-2";
import { 
    Chart,
    CategoryScale,
    LinearScale,
    PointElement,
    LineElement,
    Title,
    Tooltip,
    Legend
} from "chart.js";

function CapstoneChart (props) {
    Chart.register (
        CategoryScale, 
        LineElement, 
        LinearScale, 
        PointElement,
        Title,
        Tooltip,
        Legend
    );
    
    const labels = props.labels;
    
    const options = {
        reponsive: true,
        plugins: {
            legend: {
                position: 'top'
            },
            title: {
                display: true,
                text: props.title
            }
        }
    }

    const serie = {
        labels,
        datasets: [
            {
                label: props.legend,
                data: props.serie,
                borderColor: props.color,
                backgroundColor: props.backgroundColor
            }
        ]
    };

    return (
        <Line options={options} data={serie} />
    );
}

export default CapstoneChart;