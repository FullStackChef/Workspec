import { dia, shapes } from '@joint/core';

let graph: dia.Graph;
let paper: dia.Paper;
let selectedElement: dia.Element | null = null;

export function initializeJoint(canvasId: string, dotNetHelper: any) {
    graph = new dia.Graph();
    paper = new dia.Paper({
        el: document.getElementById(canvasId)!,
        model: graph,
        width: '100%',
        height: '600px',
        gridSize: 10,
        drawGrid: true,
    });

    paper.on('element:pointerdown', (elementView) => {
        selectedElement = elementView.model;
        const label = selectedElement.attr('label/text');
        dotNetHelper.invokeMethodAsync('OnScreenSelected', label);
    });
}

export function addScreenCentered(label: string) {
    const screen = new shapes.standard.Rectangle({ size: { height: 100, width: 200 } });
    screen.attr({
        body: { fill: '#4A90E2', stroke: '#333', rx: 5, ry: 5 },
        label: { text: label, fill: '#FFFFFF', fontSize: 16 }
    });

    const canvasRect = paper.el.getBoundingClientRect();
    const centerX = canvasRect.width / 2 - 100;
    const centerY = canvasRect.height / 2 - 60;

    screen.position(centerX, centerY);
    screen.addTo(graph);
}
