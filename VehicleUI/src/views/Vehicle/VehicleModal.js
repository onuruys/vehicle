import React, { useState } from "react";
import PropTypes from "prop-types";
//import { makeStyles } from "@material-ui/core/styles";

// core components
import Modal from "react-bootstrap/Modal";
import Dialog from "@material-ui/core/Dialog";
import DialogTitle from "@material-ui/core/DialogTitle";
import DialogContent from "@material-ui/core/DialogContent";
import DialogActions from "@material-ui/core/DialogActions";
import Button from "react-bootstrap/Button";
import Form from "react-bootstrap/Form";
import { InputLabel, MenuItem, Select } from "@material-ui/core";

const VehicleModal = (props) => {
  const [data, setData] = useState(props.detail);

  const inputChange = (name, value) => {
    setData({ ...data, [name]: value });
  };

  return (
    <Dialog
      open={true}
      keepMounted
      onClose={() => props.handleClose()}
      aria-labelledby="classic-modal-slide-title"
      aria-describedby="classic-modal-slide-description"
    >
      <DialogTitle>
        <Modal.Title id="contained-modal-title-vcenter">
          Vehicle - {props.detail.VehicleId ? "Update" : "Create"}
        </Modal.Title>
      </DialogTitle>
      <DialogContent>
        <Form style={{ width: "300px" }}>
          <Form.Group controlId="VehicleName">
            <Form.Label>Vehicle Name</Form.Label>
            <Form.Control
              type="text"
              value={data.Name}
              onChange={(e) => inputChange("Name", e.target.value)}
              placeholder="Vehicle Name"
            />
          </Form.Group>
          <Form.Group
            controlId="BrandId"
            style={{
              margin: "16px 0",
            }}
          >
            <InputLabel id="model-select-label">Brand</InputLabel>
            <Select
              fullWidth
              labelId="model-select-label"
              id="model-select"
              onChange={(e) => inputChange("ModelId", e.target.value)}
              value={data.ModelId}
            >
              {props.models?.map((model) => (
                <MenuItem key={model.ModelId} value={model.ModelId}>
                  {model.ModelName}
                </MenuItem>
              ))}
            </Select>
          </Form.Group>

          <Form.Group controlId="Plate">
            <Form.Label>Plate</Form.Label>
            <Form.Control
              type="text"
              value={data.Plate}
              onChange={(e) => inputChange("Plate", e.target.value)}
              placeholder="Plate"
            />
          </Form.Group>

          <Form.Group controlId="ModelYear">
            <Form.Label>Model Year</Form.Label>
            <Form.Control
              type="number"
              value={data.ModelYear}
              onChange={(e) => inputChange("ModelYear", e.target.value)}
              placeholder="Model Year"
            />
          </Form.Group>

          <Form.Group controlId="Color">
            <Form.Label>Color</Form.Label>
            <Form.Control
              type="text"
              value={data.Color}
              onChange={(e) => inputChange("Color", e.target.value)}
              placeholder="Color"
            />
          </Form.Group>

          <Form.Group controlId="Active">
            <Form.Check
              type="checkbox"
              checked={data.Active}
              onChange={(e) => inputChange("Active", e.target.checked)}
              label="Active"
            />
          </Form.Group>
        </Form>
      </DialogContent>
      <DialogActions>
        <Button
          onClick={props.handleClose}
          variant="contained"
          color="secondary"
        >
          Close
        </Button>
        <Button color="primary" onClick={() => props.addOrUpdateVehicle(data)}>
          Save
        </Button>
      </DialogActions>
    </Dialog>
  );
};
VehicleModal.propTypes = {
  open: PropTypes.bool.isRequired,
  handleClose: PropTypes.func.isRequired,
  addOrUpdateVehicle: PropTypes.func,
  detail: PropTypes.object.isRequired,
  models: PropTypes.array,
};
export default VehicleModal;
